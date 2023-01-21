import { RouteConfig } from "../common/common.route.config"
import express, { Application, Request, Response } from "express"
import TwitterReportController from "./twitterreport.controller"
import { body, check } from "express-validator"
import { requiresAuth } from "express-openid-connect"
import AuthMiddleware from "../authentication/authentication.middleware"



export class TwitterReportRoutes extends RouteConfig {
  constructor(app: Application) {
    super(app, "TwitterReportRoutes")
  }
  configureRoutes() {

    this.app.route(`/twitter-report`).get([AuthMiddleware.authenticateAccessToken, AuthMiddleware.checkRoles(["Admin"]), TwitterReportController.getTwitterReports])

    this.app.get(`/twitter-report/:id`, [
      check('id').isInt().withMessage("The 'id' parameter must be an integer"), ],
      [AuthMiddleware.authenticateAccessToken, AuthMiddleware.checkRoles(["Admin"]), TwitterReportController.getTwitterReportByCampaignId])
    

    this.app.post(`/twitter-report`, [
      body('campaign_id').isInt().withMessage("The 'campaign_id' parameter must be an integer"),
      body('tweets').isInt().withMessage("The 'tweets' parameter must be an integer"),
      body('retweets').isInt().withMessage("The 'retweets' parameter must be an integer"),
    ], [AuthMiddleware.authenticateAccessToken, AuthMiddleware.checkRoles(["Admin"]), TwitterReportController.addTwitterReport])

    this.app.put(`/twitter-report/:id`, [
      check('id').isInt().withMessage("The 'id' parameter must be an integer"),
      body('tweets').isInt().withMessage("The 'tweets' parameter must be an integer"),
      body('retweets').isInt().withMessage("The 'retweets' parameter must be an integer"),
    ], [AuthMiddleware.authenticateAccessToken, AuthMiddleware.checkRoles(["Admin"]), TwitterReportController.updateTwitterReport])
    
    return this.app
  }
}