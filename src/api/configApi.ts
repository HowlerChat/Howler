import { getConfig } from "../config/config";

// -----------------
// TYPES - These are the config objects, ultimately fanning out from the user identifier.
// This enables multi-user configurations in the future.
export type UserAuthorizers = {
    authorizers: { [serverId: string]: string };
};

export type UserData = {
    config: { [option: string]: any };
};

export type UserServers = {
    serverIds: string[];
};

export type UserSpaces = {
    spaceIds: string[];
};

// -----------------
// APIs - These are the configuration calls, on a per-user basis. Depending on
// configuration mode, this will either use local storage or call out to the configuration
// service.
export const getAuthorizerConfig = (userId: string) =>
    wrapGetRequest<UserAuthorizers>(`/config/${userId}/authorizers`);

export const getUserConfig = (userId: string) =>
    wrapGetRequest<UserData>(`/config/${userId}/data`);

export const getServersConfig = (userId: string) =>
    wrapGetRequest<UserServers>(`/config/${userId}/servers`);

export const getSpacesConfig = (userId: string) =>
    wrapGetRequest<UserSpaces>(`/config/${userId}/spaces`);

export const postAuthorizerConfig = (userId: string, authorizers: UserAuthorizers) =>
    wrapPostRequest<UserAuthorizers>(`/config/${userId}/authorizers`, authorizers);

export const postUserConfig = (userId: string, data: UserData) =>
    wrapPostRequest<UserData>(`/config/${userId}/data`, data);

export const postServersConfig = (userId: string, servers: UserServers) =>
    wrapPostRequest<UserServers>(`/config/${userId}/servers`, servers);

export const postSpacesConfig = (userId: string, spaces: UserSpaces) =>
    wrapPostRequest<UserSpaces>(`/config/${userId}/spaces`, spaces);

const wrapGetRequest = function<T>(path: string) {
    return (token: string) => () => fetch(getConfig().howlerConfigApiUrl + `/${getConfig().apiVersion}` + path,
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
};

const wrapPostRequest = function<T>(path: string, body: any) {
    return (token: string) => () => fetch(getConfig().howlerConfigApiUrl + `/${getConfig().apiVersion}` + path,
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
};
