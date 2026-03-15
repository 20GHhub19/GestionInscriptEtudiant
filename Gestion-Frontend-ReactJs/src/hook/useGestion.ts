import { useContext } from "react";
import GestionContext  from "../context/GetionProvider";




export const useGestion=()=>{

    return useContext(GestionContext)
}


