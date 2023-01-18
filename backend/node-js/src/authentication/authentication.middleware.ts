import { NextFunction, Response } from 'express';
import { Request, expressjwt } from "express-jwt";
import jwtAuthz from "express-jwt-authz";
import dotenv from "dotenv"
const jwt = require('express-jwt');
const jwks = require('jwks-rsa');

dotenv.config({})
class AuthMiddleware {

    constructor() { }
    
    // public tokenMiddleware = async (req: Request, res: Response, next: NextFunction) => {
    //     let token: string | undefined = req.headers["authorization"]?.toString();
    //     if (!token) {
    //         return res.status(403).send("A token is required for authentication");
    //     }
    //     //req.headers['Authorization'] = `${req.oidc.accessToken?.token_type} ${req.oidc.accessToken?.access_token}`;
    //     req.headers['Authorization'] = token;
    //     console.log(req.headers['Authorization']);
    //     //req.auth = token;
    //     // next();
    //     try {
    //         this.authenticateAccessToken(req, res, next)
    //             .then(() => {
    //                 console.log("auth-then");
    //                 return next()
    //             })
    //             .catch(() => {
    //                 console.log("auth-catch");
    //                 return res.status(401).send('Unauthorized');
    //             })
    //         //const permissions = await this.checkPermissions(req, res, next);
    //     }
    //     catch (err) {
    //         console.log("auth-catch2");
    //         return res.status(401).send('Unauthorized');
    //     }
    //     //return next();
    // }

    public checkPermissions = (permisions: string[]) => jwtAuthz(
        permisions,
        {
            customScopeKey: "permissions",
            customUserKey: 'auth',
            failWithError: true
        }
    );

    public checkRoles = (roles: string[]) => jwtAuthz(
        roles,
        {
            customScopeKey: "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
            customUserKey: 'auth',
            failWithError: true
        }
    );

    public authenticateAccessToken = expressjwt({
        secret: jwks.expressJwtSecret({
            cache: true,
            rateLimit: true,
            jwksRequestsPerMinute: 5,
            jwksUri: 'https://dev-jr7t62fw3n4cftwt.us.auth0.com/.well-known/jwks.json'
        }),
        audience: "1RAPuIXc8O65zsmujQQUnHixw0cdCsLY",
        issuer: "https://dev-jr7t62fw3n4cftwt.us.auth0.com/",
        algorithms: ['RS256'],
        credentialsRequired: true
    });

}

export default new AuthMiddleware();

// function getTokenMiddleware(req: Request, res: Response, next: NextFunction) {
//   // Initialize the Auth0 client.
//   const auth0 = new AuthenticationClient({
//     domain: 'YOUR_AUTH0_DOMAIN',
//     clientId: 'YOUR_CLIENT_ID',
//     clientSecret: 'YOUR_CLIENT_SECRET'
//   });

//   // Authenticate the user.
//   auth0.oauth?.passwordGrant({
//     username: 'user@example.com',
//     password: 'password',
//     audience: 'https://YOUR_AUTH0_DOMAIN/api/v2/',
//     scope: 'read:users'
//   })
//     .then((response) => {
//       // Get the user information.
//       auth0.users?.getInfo(response.access_token, (error, user) => {
//         if (error) {
//           console.error(error);
//         //   return res.status(401).send('Unauthorized');
//         }
//         Object.defineProperty(req, 'user', {
//           value: user,
//           writable: true,
//           enumerable: true,
//           configurable: true
//         });

//         // Get the JWT.
//         const token = response.access_token;

//         // Set the Authorization header.
//         res.setHeader('Authorization', `Bearer ${token}`);

//         // Call the next middleware.
//         next();
//       });
//     })
//     .catch((error) => {
//       console.error(error);
//       //res.status(401).send('Unauthorized');
//     });
// }
