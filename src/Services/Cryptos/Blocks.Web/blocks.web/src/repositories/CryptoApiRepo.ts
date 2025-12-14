import type {ApiResponse, BlockTransaction, Chain, PagedResponse} from "../types";
import AppSettings from "../AppSettings.ts";

export default class CryptoApiRepo {
    static async getChains(): Promise<ApiResponse<Chain>> {
        const url = `${AppSettings.ApiUrl}/crypto-api/crypto/chains`;
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    }

    static async getTransactions(coin: string, chain: string, page: number, pageSize:number): Promise<PagedResponse<BlockTransaction>> {
        const url = `${AppSettings.ApiUrl}/crypto-api/crypto/transactions/${coin}/${chain}?pageSize=${pageSize}&page=${page}`;
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    }
}