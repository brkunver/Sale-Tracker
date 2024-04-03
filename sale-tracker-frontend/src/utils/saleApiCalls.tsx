type Sale = {
  saleId: number
  saledOn: string
  productId: number
  productName: string
  productDescription: string
  productPrice: number
  productCreatedOn: string
  productUpdatedOn: string
  productImageUrl: string
}

type SaleData = {
  success: boolean
  message: string
  data: Sale[]
}

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
