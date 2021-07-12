import * as signalR from '@microsoft/signalr';
import { getConfig } from "../config/config";

export const connect = function(token: string) {
    return () => {
        let connection = new signalR.HubConnectionBuilder()
            .withAutomaticReconnect()
            .withUrl(getConfig().howlerApiUrl + "/howler", { accessTokenFactory: () => token }).build();
        return connection.start().then(() => { return { connection: connection, error: null }; });
    };
};