import type { Sale, SaleData } from "@/types/saleTypes"

enum queryNames {
  page = "page",
  count = "count",
}

function getTokenHeader() {
  let bearerToken = localStorage.getItem("token")
  let headers = new Headers()
  headers.append("Authorization", "Bearer " + bearerToken)
  return headers
}

export async function getAllSales(page?: number, count?: number) {
  let url = import.meta.env.VITE_API_URL + "/api/sale"
  let params = new URLSearchParams()
  if (page) {
    params.append(queryNames.page, page.toString())
  }
  if (count) {
    params.append(queryNames.count, count.toString())
  }
  url += "?" + params.toString()
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
