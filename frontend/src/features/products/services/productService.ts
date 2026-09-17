import axios from "axios";
import { api } from "../../../services/Api";

export interface productList {
  name: string;
}

async function getAll(): Promise<Error | productList[]> {
  try {
    const response = await api.get("catalog/products");
    return response.data;
  } catch (err) {
    if (axios.isAxiosError(err)) {
      return new Error(err.response?.data?.error ?? "Erro ao listar produtos.");
    }

    return new Error("Erro inesperado");
  }
}

export const productService = { getAll };
