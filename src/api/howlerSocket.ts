import { getConfig } from "../config/config";

const wrapRequestForServer = function<T>(wrapRequestCreator: (token: string) => () => Promise<T>) {
    return (getTokenForServer: (serverId: string) => string) =>
        (serverId: string) => {
            const token = getTokenForServer(serverId);
            return wrapRequestCreator(token);
        };
    };

export const connect = wrapRequestForServer((_token: string) => {
    return async () => {
        // let connection = new WebSocket()
            // .withAutomaticReconnect()
            // .withUrl(getConfig().howlerApiUrl + "/howler", { accessTokenFactory: () => token }).build();
        // return connection.start().then(() => { return { connection: connection, error: null }; });
    };
});
