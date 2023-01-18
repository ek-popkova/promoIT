import { RouteConfig } from "../common/common.route.config"
import express, { Application, Request, Response } from "express"
import TwitterReportController from "./twitterreport.controller"
import { body, check } from "express-validator"
import { requiresAuth } from "express-openid-connect"


export class TwitterReportRoutes extends RouteConfig {
  constructor(app: Application) {
    super(app, "TwitterReportRoutes")
  }
  configureRoutes() {

    this.app.route(`/twitter-report`).get(requiresAuth(),[TwitterReportController.getTwitterReports])

    this.app.get(`/twitter-report/:id`, [requiresAuth(),
      check('id').isInt().withMessage("The 'id' parameter must be an integer"), ],
      [TwitterReportController.getTwitterReportByCampaignId])
    

    this.app.post(`/twitter-report`, [requiresAuth(),
      body('campaign_id').isInt().withMessage("The 'campaign_id' parameter must be an integer"),
      body('tweets').isInt().withMessage("The 'tweets' parameter must be an integer"),
      body('retweets').isInt().withMessage("The 'retweets' parameter must be an integer"),
    ], [TwitterReportController.addTwitterReport])

    this.app.put(`/twitter-report/:id`, [requiresAuth(),
      check('id').isInt().withMessage("The 'id' parameter must be an integer"),
      body('tweets').isInt().withMessage("The 'tweets' parameter must be an integer"),
      body('retweets').isInt().withMessage("The 'retweets' parameter must be an integer"),
    ], [TwitterReportController.updateTwitterReport])
    
    return this.app
  }
}