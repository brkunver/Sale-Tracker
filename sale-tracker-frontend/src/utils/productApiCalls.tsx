type Product = {
  productId: number
  name: string
  description: string
  price: number
  imageUrl: string
  createdOn: Date
  updatedOn: Date
}

type ProductData = {
  success: boolean
  message: string
  data: [Product]
}

let bearerToken = localStorage.getItem("token")
let headers = new Headers()
headers.append("Authorization", "Bearer " + bearerToken)

export async function getAllProducts(page?: number, count?: number) {
  let url = import.meta.env.VITE_API_URL + "/api/product?page=" + (page ?? 1) + "&count=" + (count ?? 5)
  let response = await fetch(url, {
    headers: headers,
  })
  if (!response.ok) {
    throw new Error(response.statusText)
  }
  let data = await response.json()
  return data as ProductData
}

export function getImageUrl(imagename: string) {
  let url = import.meta.env.VITE_API_URL + "/uploads/" + imagename
  return url
}

export async function getSingleProduct(productId: number) {
  let url = import.meta.env.VITE_API_URL + "/api/product/" + productId
  let response = await fetch(url, {
    headers: headers,
  })
  if (!response.ok) {
    throw new Error(response.statusText)
  }
  let data = await response.json()
  return data.data as Product
}

export async function getImageUrlById(productId: number) {
  let product = await getSingleProduct(productId)
  return getImageUrl(product.imageUrl)
}
