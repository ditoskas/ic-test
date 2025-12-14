import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { BlocksProvider } from "./contexts/BlocksContext/BlocksContext";

createRoot(document.getElementById('root')!).render(
  <StrictMode>
      <BlocksProvider>
          <App />
      </BlocksProvider>
  </StrictMode>,
)
