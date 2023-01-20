
import { RouteConfig } from "../common/common.route.config"
import express, { Application, Request, Response } from "express"
import SocialActivistController from "./socialsctivist.controller"
import { body, check } from "express-validator"
import { requiresAuth } from "express-openid-connect"
import jwtAuthz from "express-jwt-authz"
import AuthMiddleware from "../authentication/authentication.middleware"



export class SocialActivistRoutes extends RouteConfig {
  constructor(app: Application) {
    super(app, "SocialActivistsRoutes")
    }
    
  configureRoutes() {

    //this.app.route(`/social-activists`).get([SocialActivistController.getSocialActivists]);
    //this.app.route(`/social-activists`).get([AuthMiddleware.authenticateAccessToken, AuthMiddleware.checkRoles(["Admin"]), SocialActivistController.getSocialActivists])
    this.app.route(`/social-activists`).get([SocialActivistController.getSocialActivists])

    this.app.route(`/social-activist-id/:id`).get([SocialActivistController.getSocialActivistIdByUserId])

    //this.app.route(`/social-activists/:id`).get([SocialActivistController.getSocialActivistById])
    this.app.get(`/social-activists/:id`, [
      check('id').isInt().withMessage("The 'id' parameter must be an integer"), ],
      [SocialActivistController.getSocialActivistById])
    
    //this.app.route(`/social-activists-twitter/:twitter`).get([SocialActivistController.getSocialActivistByTwitter])
    this.app.get(`/social-activists-twitter/:twitter`, [
      check('twitter').isLength({ min: 4, max: 15 }).isAlphanumeric('en-US', {ignore: '_'}).withMessage("The 'twitter' parametr must be a valid twitter account")
    ], [SocialActivistController.getSocialActivistByTwitter])

    //this.app.route(`/social-activists`).post([SocialActivistController.addSocialActivist])
    this.app.post(`/social-activists`, [
      //body('user_id').isInt().withMessage("The 'user_id' parameter must be an integer"),
      body('email').isEmail().withMessage("The 'email' parameter must be valid"),
      body('address').isLength({max: 50}).withMessage("The 'address' parameter must be not more than 50 characters long"),
      body('phone').isNumeric().withMessage("The 'phone' parameter must be numeric"),
      body('twitter').isLength({ min: 4, max: 15 }).isAlphanumeric('en-US', {ignore: '_'}).withMessage("The 'twitter' parametr must be a valid twitter account"),
    ], [SocialActivistController.addSocialActivist])

    //this.app.route(`/social-activists/:id`).put([SocialActivistController.updateSocialActivist])
    this.app.put(`/social-activists/:id`, [
      check('id').isInt().withMessage("The 'id' parameter must be an integer"),
      body('user_id').isInt().withMessage("The 'user_id' parameter must be an integer"),
      body('email').isEmail().withMessage("The 'email' parameter must be valid"),
      body('address').isLength({max: 50}).withMessage("The 'address' parameter must be not more than 50 characters long"),
      body('phone').isNumeric().withMessage("The 'phone' parameter must be numeric"),
      body('twitter').isLength({ min: 4, max: 15 }).isAlphanumeric('en-US', {ignore: '_'}).withMessage("The 'twitter' parametr must be a valid twitter account"),
    ], [SocialActivistController.updateSocialActivist])

    this.app.delete(`/social-activists/:id`, [
      check('id').isInt().withMessage("The 'id' parameter must be an integer"),],
      [SocialActivistController.deleteSocialActivist])
    
    return this.app
  }
}