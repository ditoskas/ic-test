import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css'
import MainLayout from "./layouts/MainLayout/MainLayout.tsx";
import { useBlocks } from "./contexts/BlocksContext/BlocksContext";
import MainLoader from "./components/Loaders/MainLoader.tsx";
import {useEffect} from "react";
import BlockHashTable from "./components/Tables/BlockHashTable/BlockHashTable.tsx";

function App() {
    const { pagedBlocks, loading, loadBlockChains } = useBlocks();
    const totalRecords = pagedBlocks?.payload.totalRecords ?? 0;

    useEffect(() => {
        loadBlockChains();
    }, []);
    return (
      <MainLayout numberOfRecords={totalRecords}>
          {loading && <MainLoader />}
          {!loading && <BlockHashTable pageInfo={pagedBlocks} />}
      </MainLayout>
  )
}

export default App
