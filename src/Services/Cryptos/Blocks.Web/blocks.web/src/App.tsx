import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css'
import MainLayout from "./layouts/MainLayout/MainLayout.tsx";
import { useBlocks } from "./contexts/BlocksContext/BlocksContext";
import MainLoader from "./components/Loaders/MainLoader.tsx";
import type {BlockTransaction, PagedResponse} from "./types";
import CryptoApiRepo from "./repositories/CryptoApiRepo.ts";
import {useEffect} from "react";
import BlockHashTable from "./components/Tables/BlockHashTable/BlockHashTable.tsx";

function App() {
    const { pagedBlocks, loading, setPagedBlocks, setLoading } = useBlocks();
    const totalRecords = pagedBlocks?.payload.totalRecords ?? 0;
    console.log(loading);
    async function fetchPageBlocks() {
        setLoading(true);
        const response : PagedResponse<BlockTransaction> = await CryptoApiRepo.getTransactions("btc", "main", 1, 10);
        console.log(response);
        setPagedBlocks(response);
        setLoading(false);
    }

    useEffect(() => {
        fetchPageBlocks();
    }, []);
    return (
      <MainLayout numberOfRecords={totalRecords}>
          {loading && <MainLoader />}
          {!loading && <BlockHashTable pageInfo={pagedBlocks} />}
      </MainLayout>
  )
}

export default App
