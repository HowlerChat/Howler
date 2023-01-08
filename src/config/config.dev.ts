export const getDevConfig = function() {
    return {
        cognito: {
            aws_cognito_region: "us-west-2",
            aws_user_pools_id: "us-west-2_8ZX6GevSf",
            aws_user_pools_web_client_id: "6b75ooll3b86ugauhu22vj39ra",
        },
        howlerApiUrl: "http://localhost:5000",
        howlerIndexApiUrl: "http://localhost:5001",
        howlerConfigApiUrl: "http://localhost:5002",
        gatewayApiUrl: "https://gateway.howler.chat",
        apiVersion: 'v1', // This is our placeholder until official launch, where things move into a date-basis
    }
}