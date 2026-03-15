import { createContext, type ReactNode } from "react"

 

const GestionContext=createContext({});

interface GestionProviderProps {
  children: ReactNode
}


 const GestionProvider = ({children}:GestionProviderProps) => {
 
 
  const hola="hola mundo"
  
  return (
    <GestionContext.Provider 
      value={{
             hola

      }}
        
    >{children}</GestionContext.Provider>



   )
}
export {

    GestionProvider
}
export default GestionContext;