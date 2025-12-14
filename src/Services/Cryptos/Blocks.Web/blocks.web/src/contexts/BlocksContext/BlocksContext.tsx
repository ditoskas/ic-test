import {createContext, type ReactNode, useContext, useEffect, useState} from "react";
import type {Chain, PagedResponse, BlockTransaction, ApiResponse, ErrorRecord} from "../../types";
import CryptoApiRepo from "../../repositories/CryptoApiRepo.ts";

export interface BlocksState {
    pagedBlocks: PagedResponse<BlockTransaction> | null;
    loading: boolean;
    error: ErrorRecord[];
    blockChains: Chain[];
    selectedChainId?: number | null;
    selectedPage: number;
    selectedPageSize: number;
}

export interface BlocksContextValue extends BlocksState {
    loadBlockChains(): void;
    setSelectedChainId(value: number | null): void;
    setSelectedPage(value: number): void;
    setSelectedPageSize(value: number): void;
}

const DefaultSelectedPage = 1;
const DefaultSelectedPageSize = 10;

const BlocksContext = createContext<BlocksContextValue | undefined>(undefined);

export function BlocksProvider({ children }: { children: ReactNode }) {
    const [pagedBlocks, setPagedBlocksState] = useState<PagedResponse<BlockTransaction> | null>(null);
    const [loading, setLoadingState] = useState<boolean>(false);
    const [error, setErrorState] = useState<ErrorRecord[]>([]);
    const [blockChains, setBlockChainsState] =  useState<Chain[]>([]);
    const [selectedChainId, setSelectedChainIdState] = useState<number | null>(null);
    const [selectedPage, setSelectedPageState] = useState<number>(DefaultSelectedPage);
    const [selectedPageSize, setSelectedPageSizeState] = useState<number>(DefaultSelectedPageSize);

    async function fetchChains() {
        setLoadingState(true);
        const response : ApiResponse<Chain> = await CryptoApiRepo.getChains();
        if (!response.success) {
            setErrorState(response.message);
            setBlockChainsState([]);
        } else {
            setErrorState([]);
            setBlockChainsState(response.payload ?? []);
        }
        setLoadingState(false);
    }

    async function fetchTransactions(coin: string, chain: string) {
        setLoadingState(true);
        console.log(coin, chain, selectedPage, selectedPageSize);
        const response : PagedResponse<BlockTransaction> = await CryptoApiRepo.getTransactions(coin, chain, selectedPage, selectedPageSize);
        console.log(response);
        if (!response.success) {
            setErrorState(response.messages);
            setPagedBlocksState(null);
        } else {
            setErrorState([]);
            setPagedBlocksState(response);
        }
        setLoadingState(false);
    }


    function setSelectedChainId(value: number) {
        const selectedChain = blockChains.find(c => c.id === value) ?? null;
        if (!selectedChain) {
            throw new Error("Selected chain not found");
        }
        setSelectedChainIdState(value);
        setSelectedPageState(DefaultSelectedPage);
        setSelectedPageSizeState(DefaultSelectedPageSize);
        fetchTransactions(selectedChain.coin, selectedChain.chain);
    }

    function setSelectedPage(value: number) {
        if (value <= 0) {
            throw new Error("Selected page must be a positive integer");
        }
        setSelectedPageState(value);
    }

    function setSelectedPageSize(value: number) {
        if (value <= 0) {
            throw new Error("Selected page size must be a positive integer");
        }
        setSelectedPageSizeState(value);
    }

    function loadBlockChains() {
        fetchChains();
    }

    useEffect(() => {
        function reloadTransactions() {
            const selectedChain = blockChains.find(c => c.id === selectedChainId) ?? null;
            if (!selectedChain) {
                throw new Error("Selected chain not found");
            }
            fetchTransactions(selectedChain.coin, selectedChain.chain);
        }
        if (selectedChainId != null) {
            reloadTransactions();
        }
    }, [selectedChainId, selectedPage, selectedPageSize]);

    const value: BlocksContextValue = {
        pagedBlocks,
        blockChains,
        loading,
        error,
        selectedChainId,
        selectedPage,
        selectedPageSize,
        setSelectedPageSize,
        setSelectedPage,
        setSelectedChainId,
        loadBlockChains
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
