export interface Chain {
    id: number;
    coin: string;
    chain: string;
    name: string;
}

export interface BlockTransaction {
    id: number;
    hash: string;
    height: number;
    chain: string;
    total: number;
    fees: number;
    size: number;
    vsize: number;
    ver: number;
    time: string;          // ISO date string
    receivedTime: string;  // ISO date string
    coinbaseAddr: string;
    relayedBy: string;
    bits: number;
    nonce: number;
    nTx: number;
    prevBlock: string;
    mrklRoot: string;
    txids: string[];
    depth: number;
    prevBlockUrl: string;
    txUrl: string;
    nextTxids: string;
    createdAt: string;     // ISO date string
    updatedAt: string;     // ISO date string
}

export interface ErrorRecord {
    fieldName: string;
    message: string;
}

export interface ApiResponse<T> {
    success: boolean;
    message: ErrorRecord[];
    payload?: T[];
}

export interface PagedPayload<T> {
    pageNumber: number;
    pageSize: number;
    totalRecords: number;
    totalPages: number;
    data: T[];
}

export interface PagedResponse<T> {
    success: boolean;
    messages: ErrorRecord[];       // or a specific message type if you have one
    payload: PagedPayload<T>;
}