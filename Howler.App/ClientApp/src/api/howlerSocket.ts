import * as signalR from '@microsoft/signalr';
import { getConfig } from "../config/config";

const wrapRequestForServer = function<T>(wrapRequestCreator: (token: string) => () => Promise<T>) {
    return (getTokenForServer: (serverId: string) => string) =>
        (serverId: string) => {
            const token = getTokenForServer(serverId);
            return wrapRequestCreator(token);
        };
    };

export const connect = wrapRequestForServer((token: string) => {
    return () => {
        let connection = new signalR.HubConnectionBuilder()
            .withAutomaticReconnect()
            .withUrl(getConfig().howlerApiUrl + "/howler", { accessTokenFactory: () => token }).build();
        return connection.start().then(() => { return { connection: connection, error: null }; });
    };
});
