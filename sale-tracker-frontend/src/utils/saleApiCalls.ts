import type { Sale, SaleData } from "@/types/saleTypes"

function getTokenHeader() {
  let bearerToken = localStorage.getItem("token")
  let headers = new Headers()
  headers.append("Authorization", "Bearer " + bearerToken)
  return headers
}

export async function getAllSales(page?: number, count?: number) {
  let url = import.meta.env.VITE_API_URL + "/api/sale?page=" + (page ?? 1) + "&count=" + (count ?? 5)
  let response = await fetch(url, {
    headers: getTokenHeader(),
  })
  if (!response.ok) {
    throw new Error(response.statusText)
  }
  let data = await response.json()
  return data as SaleData
}

export async function getSaleById(saleId: string) {
  let url = import.meta.env.VITE_API_URL + "/api/sale/" + saleId
  let response = await fetch(url, {
    headers: getTokenHeader(),
  })
  if (!response.ok) {
    throw new Error(response.statusText)
  }
  let data = await response.json()
  return data as Sale
}
