import { getConfig } from "../config/config";
import { PagedResponse } from "./baseTypes";

// -----------------
// TYPES
type IndexedSpace = {
    spaceId: string;
    spaceName: string;
    description: string;
    vanityUrl: string;
    serverUrl: string;
    createdDate: Date;
    modifiedDate: Date;
}

// -----------------
// APIs
export const searchIndexedSpaces = (search: string, pageToken?: string | undefined) =>
    wrapRequestForServer<PagedResponse<IndexedSpace>>(wrapGetRequest(`/spaces/search`, pageToken));

export const getSpaceByInviteUrl = (inviteUrl: string) =>
    wrapRequestForServer<IndexedSpace>(wrapGetRequest(`/spaces/${inviteUrl.split('/').slice(-1)[0]}`));

export const getSpaceByVanityUrl = (vanityUrl: string) =>
    wrapRequestForServer<IndexedSpace>(wrapGetRequest(`/${vanityUrl.split('/').slice(-1)[0]}`));

const wrapRequestForServer = function<T>(wrapRequestCreator: (token: string) => () => Promise<T>) {
    return (getTokenForServer: (serverId: string) => string) =>
        (serverId: string) => {
            const token = getTokenForServer(serverId);
            return wrapRequestCreator(token);
        };
};

const wrapGetRequest = function<T>(path: string, pageToken?: string | undefined) {
    return (token: string) => () => fetch(getConfig().howlerIndexApiUrl + `/${getConfig().apiVersion}` + path +
        (!!pageToken ? `?pageToken=${pageToken}` : ""),
    {
        method: 'GET',
        mode: "cors",
        headers: {
            'Accept': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    }).then(response => {
        return response.json() as Promise<T>;
    });
}

const wrapPostRequest = function<T>(path: string, body: any) {
    return (token: string) => () => fetch(getConfig().howlerIndexApiUrl + `/${getConfig().apiVersion}` + path,
    {
        method: 'POST',
        mode: "cors",
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(body)
    }).then(response => {
        return response.json() as Promise<T>;
    });
}