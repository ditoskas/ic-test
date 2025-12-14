import {createContext, type ReactNode, useContext, useState} from "react";
import type {Chain, PagedResponse, BlockTransaction, ApiResponse} from "../../types";
import CryptoApiRepo from "../../repositories/CryptoApiRepo.ts";

export interface BlocksState {
    pagedBlocks: PagedResponse<BlockTransaction> | null;
    loading: boolean;
    error: string | null;
    blockChains: Chain[];
    selectedChainId?: number | null;
    selectedPage?: number | null;
    selectedPageSize?: number | null;
}

export interface BlocksContextValue extends BlocksState {
    setSelectedChainId(value: number | null): void;
    setSelectedPage(value: number | null): void;
    setSelectedPageSize(value: number | null): void;
    // setPagedBlocks: (value: PagedResponse<BlockTransaction>) => void;
    // clearBlocks: () => void;
    // setLoading: (value: boolean) => void;
    // setError: (value: string | null) => void;
    // setBlockChains: (value: Chain[]) => void;
    // clearBlockChains: () => void;
}

const BlocksContext = createContext<BlocksContextValue | undefined>(undefined);

export function BlocksProvider({ children }: { children: ReactNode }) {
    const [pagedBlocks, setPagedBlocksState] = useState<PagedResponse<BlockTransaction> | null>(null);
    const [loading, setLoadingState] = useState<boolean>(false);
    const [error, setErrorState] = useState<string | null>(null);
    const [blockChains, setBlockChainsState] =  useState<Chain[]>([]);
    const [selectedChainId, setSelectedChainId] = useState<number | null>(null);
    const [selectedPage, setSelectedPage] = useState<number | null>(null);
    const [selectedPageSize, setSelectedPageSize] = useState<number | null>(null);

    async function fetchChains() {
        setLoading(true);
        const response : ApiResponse<Chain> = await CryptoApiRepo.getChains();
        setBlockChains(response.payload ?? []);
        setLoading(false);
    }

    function setPagedBlocks(value: PagedResponse<BlockTransaction>) {
        setPagedBlocksState(value);
    }

    function clearBlocks() {
        setPagedBlocksState(null);
        setErrorState(null);
    }

    function setLoading(value: boolean) {
        setLoadingState(value);
    }

    function setError(value: string | null) {
        setErrorState(value);
    }

    function setBlockChains(value: Chain[]) {
        setBlockChainsState(value);
    }

    function clearBlockChains() {
        setBlockChainsState([]);
    }

    const value: BlocksContextValue = {
        pagedBlocks,
        blockChains,
        loading,
        error,
        setPagedBlocks,
        clearBlocks,
        setLoading,
        setError,
        setBlockChains,
        clearBlockChains,
    };

    return (
        <BlocksContext.Provider value={value}>
            {children}
        </BlocksContext.Provider>
    );
}

export function useBlocks(): BlocksContextValue {
    const ctx = useContext(BlocksContext);
    if (!ctx) {
        throw new Error("useBlocks must be used within a BlocksProvider");
    }
    return ctx;
}
