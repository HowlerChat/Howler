import { getConfig } from "../config/config";

export type Space = {
    spaceId: string;
    spaceName: string;
    description: string;
    vanityUrl: string;
    serverUrl: string;
    iconUrl: string;
    bannerUrl: string;
    userId: string;
    defaultChannelId: string;
    createdDate: Date;
    modifiedDate: Date;
};

export type Channel = {
    channelId: string;
    spaceId: string;
    channelName: string;
    channelTopic: string;
    memberId: string;
    encryptionMethod: string;
    createdDate: Date;
    modifiedDate: Date;
};

export type Message = {
    channelId: string;
    epoch: string;
    spaceId: string;
    messageId: string;
    digestAlgorithm: string;
    nonce: string;
    createdDate: Date;
    modifiedDate: Date;
    lastModifiedHash: string;
    content: PostMessage|EventMessage|EmbedMessage;
    reactions: Reaction[];
    mentions: Mentions;
};

export type PostMessage = {
    senderId: string;
    type: "post";
    text: string;
    repliesToMessageId?: string;
};

export type EventMessage = {
    type: "event";
    text: string;
    repliesToMessageId?: string;
};

export type EmbedMessage = {
    senderId: string;
    type: "embed";
    videoUrl: string;
    width?: string;
    height?: string;
    repliesToMessageId?: string;
};

export type Reaction = {
    emojiId: string;
    spaceId: string;
    emojiName: string;
    count: number;
    memberIds: string[];
}

export type Mentions = {
    memberIds: string[];
    roleIds: string[];
    channelIds: string[];
}

// -----------------
// APIs
export const getSpace = (spaceId: string) =>
    wrapRequestForServer<Space>(wrapGetRequest(`/spaces/${spaceId}`));

export const getLocalization = function(langId: string) {
    return () => {
        function remap(pair: any[]): (vars: any) => string {
            return (vars: any): string => {
                let template = pair[0] as string;
                
                for (let name of (pair[1] as string[]))
                {
                    template = template.replace("${" + name + "}", vars[name]);
                }

                return template;
            }
        };

        return {
            "direction": "ltr",
            "langId": langId,
            "localizations": {
                "ADVANCED_SETTINGS": remap(["Advanced Settings", []]),
                "COMMUNITY_GUIDELINES": remap(["Community Guidelines", []]),
                "CREATE_SPACE": remap(["Create Space", []]),
                "CREATE_SPACE_PROMPT": remap(["Choose a name for your Space", []]),
                "CREATE_SPACE_TITLE": remap(["Create a Space", []]),
                "JOIN_SPACE": remap(["Join Space", []]),
                "JOIN_SPACE_PROMPT": remap(["Enter the vanity URL or invite URL to join", []]),
                "JOIN_SPACE_TITLE": remap(["Join a Space", []]),
                "SPACE_BANNER_ATTACHMENT": remap(["Drag and drop or click to add a Space banner", []]),
                "SPACE_CREATE_AGREEMENT_LEFT": remap(["By creating a hosted Space you agree to Howler's ", []]),
                "SPACE_CREATE_AGREEMENT_RIGHT": remap([".", []]),
                "SPACE_ICON_ATTACHMENT": remap(["Drag and drop or click to add a Space icon", []]),
                "SPACE_JOIN_AGREEMENT_LEFT": remap(["By joining a hosted Space you agree to Howler's ", []]),
                "SPACE_JOIN_AGREEMENT_RIGHT": remap([".", []]),
                "TOOLTIP_ADD_SPACE": remap(["Add a Space", []]),
                "TOOLTIP_JOIN_SPACE": remap(["Join an existing Space", []]),
                "TOOLTIP_SEARCH_SPACES": remap(["Search for Public Spaces", []]),
            }
        };
    };
}

const wrapRequestForServer = function<T>(wrapRequestCreator: (token: string) => () => Promise<T>) {
    return (getTokenForServer: (serverId: string) => string) =>
        (serverId: string) => {
            const token = getTokenForServer(serverId);
            return wrapRequestCreator(token);
        };
}

const wrapGetRequest = function<T>(path: string) {
    return (token: string) => () => fetch(getConfig().howlerApiUrl + `/${getConfig().apiVersion}` + path,
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
    return (token: string) => () => fetch(getConfig().howlerApiUrl + `/${getConfig().apiVersion}` + path,
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