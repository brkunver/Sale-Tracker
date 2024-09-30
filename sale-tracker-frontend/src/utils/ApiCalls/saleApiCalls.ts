import type { SaleData, SingleSaleData } from "@/types/saleTypes"
import { mockSaleData } from "@/utils/demoBackend/mockSaleData"
enum queryNames {
  page = "page",
  count = "count",
}

import getTokenHeader from "./getTokenHeader"
import isDemo from "../demoBackend/isDemo"
import getRandomSingleSaleData from "../demoBackend/mockSingleSaleData"

export async function getAllSales(page?: number, count?: number): Promise<SaleData> {
  if (isDemo()) {
    return mockSaleData
  }

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

export async function getSaleById(saleId: string): Promise<SingleSaleData> {
  if (isDemo()) {
    return getRandomSingleSaleData()
  } else {
    let url = import.meta.env.VITE_API_URL + "/api/sale/" + saleId
    let response = await fetch(url, {
      headers: getTokenHeader(),
    })

    if (!response.ok) {
      throw new Error(response.statusText)
    }

    let data = await response.json()
    return data as SingleSaleData
  }
}

export async function getLastSales(day?: number): Promise<number[]> {
  if (isDemo()) {
    return [1200, 675, 800, 950, 1900]
  } else {
    let url = import.meta.env.VITE_API_URL + "/api/sale/last-sales?" + "count=" + (day?.toString() ?? "7")
    let response = await fetch(url, {
      headers: getTokenHeader(),
    })
    if (!response.ok) {
      throw new Error(response.statusText)
    }
    let data = await response.json()
    return data.data as number[]
  }
}
