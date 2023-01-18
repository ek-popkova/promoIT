export enum Status {
    Active = 1,
    NotActive = 2
}

export enum Order {
    Ordered = 1,
    Shipped = 2
}

export enum AppError {
    QueryError = "QueryError",
    NoData = "NoData",
    NonPositiveInput = "NonPositiv",
    TwitterConnectionError = "TwitterError"
}