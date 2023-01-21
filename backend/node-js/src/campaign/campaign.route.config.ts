import { RouteConfig } from "../common/common.route.config"
import express, { Application, Request, Response } from "express"
import CampaignController from "./campaign.controller"
import AuthMiddleware from "../authentication/authentication.middleware"



export class CampaignRoutes extends RouteConfig {
  constructor(app: Application) {
    super(app, "CampaignRoutes")
  }
  configureRoutes() {
    this.app.route(`/campaigns`).get([AuthMiddleware.authenticateAccessToken, AuthMiddleware.checkRoles(["Admin"]), CampaignController.getAllCampaigns])
    return this.app
  }
}