import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import { RouterProvider } from 'react-router-dom'
import { GestionProvider } from './context/GetionProvider'

import { router } from './router/AppRouter'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <GestionProvider >
      <RouterProvider router={router} />
    </GestionProvider>
  </StrictMode>
)
