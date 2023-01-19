import { body } from 'express-validator';
import { check } from 'express-validator';
import { Application } from 'express';
import { RouteConfig } from "../common/common.route.config";
import sa_to_campaignController from './sa_to_campaign.controller';
import AuthMiddleware from "../authentication/authentication.middleware"

export class SA_to_campaignRoutes extends RouteConfig {
    /**
     *
     */
    constructor(app: Application) {
        super(app, "SaToCampaignRoutes");
        
    }

    configureRoutes() {
        
        this.app.put('/sa-to-campaign/:id', [
            check('id').isInt().withMessage("The 'id' parameter must be an integer"),
            body('social_activist_id').isInt().withMessage("The 'social_activist_id' must be an integer"),
            body('campaign_id').isInt().withMessage("The 'campaign_id' must be an integer"),
            body('money').isInt()?.withMessage("The 'money' must be an integer")], [AuthMiddleware.authenticateAccessToken, AuthMiddleware.checkRoles(["Social activist", "Admin"]),sa_to_campaignController.updateSAtoCampaign])
            
        return this.app
    }
}