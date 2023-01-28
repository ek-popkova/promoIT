import { AuthenticationRoutes } from './authentication/authentication.routes';
import { BcrRoutes } from './BCR/BCR.route.config';
import express, { Express, Application, Request, Response } from "express"
import * as http from "http"
import cors from "cors"
import dotenv from "dotenv"
import { RouteConfig } from "./common/common.route.config"
import { SocialActivistRoutes } from "./SA/socialactivist.route.config"
import { openConnection } from "./common/db.service"
import { CampaignRoutes } from "./campaign/campaign.route.config"
import { MoneyRoutes } from "./money/money.route.config"
import { TwitterReportRoutes } from "./twitterReport/tweeterreport.route.config"
import AuthMiddleware from './authentication/authentication.middleware'
import { requiresAuth } from 'express-openid-connect';
import { SA_to_campaignRoutes } from './SA_to_campaign/sa_to_campaign.routes';
import { MoneyTwitterHelper } from './helper/money-tweeter.helper';

const { auth } = require('express-openid-connect')

const routes: Array<RouteConfig> = []
const app: Express = express()
dotenv.config({})

app.use(
    auth({
        authRequired: false,
        auth0Logout: true,
        issuerBaseURL: process.env.ISSUER_BASE_URL,
        baseURL: process.env.BASE_URL,
        clientID: process.env.CLIENT_ID_AUTH,
        secret: process.env.SECRET,
        clientSecret: process.env.SECRET,
        idpLogout: true,
        authorizationParams: {
            response_type: 'code',
            audience: 'https://promoit/',
            scope:'openid email'
        }
  })
);

app.get('/', (req, res, next) => {
    res.send(req.oidc.isAuthenticated() ? 'Logged in' : 'Logged out');
    if ((req as any).oidc.isAuthenticated()) {
        AuthenticationRoutes.tokenType = req.oidc.accessToken?.token_type as string;
        AuthenticationRoutes.accessToken = req.oidc.accessToken?.access_token as string;
    }
})

app.get('/profile', requiresAuth(), (req, res) => {
  res.send(JSON.stringify(req.oidc.user));
});
// app.use(AuthMiddleware.authorizeAccessToken)
// app.use(requiresAuth())
app.use(express.json())
app.use(cors())

const PORT = process.env.PORT || 8000
if (process.env.DEBUG) {
  process.on("unhandledRejection", function(reason) {
    process.exit(1)
  })
} else {
}

routes.push(new AuthenticationRoutes(app))
routes.push(new SocialActivistRoutes(app))
routes.push(new CampaignRoutes(app))
routes.push(new MoneyRoutes(app))
routes.push(new TwitterReportRoutes(app))
routes.push(new BcrRoutes(app))
routes.push(new SA_to_campaignRoutes(app))

app.get("/", (req: Request, res: Response) => {
  res.send("Welcome world")
})

const server: http.Server = http.createServer(app)

server.listen(PORT, async () => {
  console.log(`Server is running on ${PORT}`)
  await openConnection.sync();
  routes.forEach((route: RouteConfig) => {
    console.log(`Routes configured for ${route.getName()}`)
  })
  //MoneyTwitterHelper.checkTweetsGiveMoney();
})