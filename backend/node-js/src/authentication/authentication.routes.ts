import { Application } from 'express';
import { auth, requiresAuth } from 'express-openid-connect';
import { RouteConfig } from "../common/common.route.config";
import dotenv from "dotenv"

dotenv.config({})

export class AuthenticationRoutes extends RouteConfig {

    public static tokenType = '';
    public static accessToken = '';

    constructor(app: Application) {
        super(app, "AuthenticationRoutes");
    }

    public configureRoutes() {

        // this.app.use(
        // auth({
        //     authRequired: false,
        //     auth0Logout: true,
        //     issuerBaseURL: process.env.ISSUER_BASE_URL,
        //     baseURL: process.env.BASE_URL,
        //     clientID: process.env.CLIENT_ID_AUTH,
        //     secret: process.env.SECRET,
        //     clientSecret: process.env.SECRET,
        //     // idpLogout: true,
        //     authorizationParams: {
        //         response_type: 'code',
        //         audience: 'http://localhost:7000',
        //         scope:'openid profile email'
        //     }
        // })
        // );

        // this.app.use(
        //     auth({
        //         authRequired: false,
        //         auth0Logout: true,
        //         secret: 'a long, randomly-generated string stored in env',
        //         baseURL: 'http://localhost:7000',
        //         clientID: 'xWapDZDkQ7At3RE8otVgBJaxm9hHxhy7',
        //         issuerBaseURL: 'https://dev-jr7t62fw3n4cftwt.us.auth0.com',
        //         clientSecret: '5UFt56LIxlSpbLthIrIjyO1oXZ4Ai8vMIQgLViKTdIFyQnbt_xiqi774RYutN5nM',
        //         idpLogout: true,
        //         authorizationParams: {
        //             response_type: 'code',
        //             audience: 'https://promoit/',
        //             scope: 'openid profile email'
        //     }
        //         })
        // );

        // this.app.get('/', (req, res, next) => {
        //     if ((req as any).oidc.isAuthenticated()) {
        //         AuthenticationRoutes.tokenType = req.oidc.accessToken?.token_type as string;
        //         AuthenticationRoutes.accessToken = req.oidc.accessToken?.access_token as string;
        //         res.send(req.oidc.isAuthenticated() ? 'Logged in' : 'Logged out');
        //     }   
        // })
        //     // requiresAuth()

        // this.app.get('/callback', (req, res) => {
        //     const { access_token } = req.query;
        //     // Use the access_token to make authenticated requests.
        // });
 
        // this.app.get('/profile', requiresAuth(), (req, res) => {
        //         res.send(JSON.stringify(req.oidc.fetchUserInfo())) 
        // })

        return this.app
    }

}