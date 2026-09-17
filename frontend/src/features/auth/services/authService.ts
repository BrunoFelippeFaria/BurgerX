import axios from "axios";
import { api } from "../../../services/Api";

interface LoginProps {
    userName: string;
    password: string;
}

async function login(props: LoginProps): Promise<void | Error> {
    try {
        await api.post('auth/login', props);
    }
    
    catch (err) {
        if (axios.isAxiosError(err)) {
            return new Error(err.response?.data?.error ?? "Erro ao fazer login");
        }
    
        return new Error("Erro inesperado");
    }
}

export const authService = { login };