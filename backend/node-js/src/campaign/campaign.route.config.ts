import { RouteConfig } from "../common/common.route.config"
import express, { Application, Request, Response } from "express"
import CampaignController from "./campaign.controller"


export class CampaignRoutes extends RouteConfig {
  constructor(app: Application) {
    super(app, "CampaignRoutes")
  }
  configureRoutes() {
    this.app.route(`/campaigns`).get([CampaignController.getAllCampaigns])
    return this.app
  }
}