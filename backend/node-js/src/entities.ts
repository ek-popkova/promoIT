import { AppError, Order } from "./enums";

export interface ISocialActivist {
  id: number,
  user_id: string,
  email: string,
  address: string,
  phone: string,
  twitter: string
}

export interface ICampaign {
  id: number,
  hashtag: string
}

export interface IMoney {
  social_activist_id: number,
  campaign_id?: number,
  campaign_hash?: string,
  money: number
}

export interface systemError {
    key: AppError;
    code: number;
    message: string;
}

export interface tweetRetweet {
  tweet: string[];
  retweet: string[];
}

export interface ITwitterReport {
  campaign_id?: number;
  campaign?: string;
  tweets: number;
  retweets: number;
}

export interface ISocialActivistTransaction {
    id: number;
    SA_id: number;
    sA_id?: number;
    bcR_id?: number;
    BCR_id: number;
    product_id: number;
    products_number: number;
    price: number;
    transaction_status_id: Order;
}

export interface ISocialActivistTransactionAdd {
    id: number;
    sA_id: number;
    bcR_id: number;
    product_id: number;
    products_number: number;
    price: number;
    transaction_status_id: Order;
}

export interface ISAtoCampaign {
    id: number;
    social_activist_id: number;
    campaign_id: number;
    money: number;
}