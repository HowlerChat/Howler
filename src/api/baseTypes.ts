export type PagedResponse<T> = {
    data: T[];
    nextPageToken: string | undefined;
};

export type ErrorResponse = {
    code: string;
    message: string;
    details: { [propertyName: string]: string };
}
