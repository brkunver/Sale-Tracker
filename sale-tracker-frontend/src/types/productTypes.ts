export type Product = {
  id: number
  name: string
  description: string
  price: number
  imageUrl: string
  createdOn: Date | string
  updatedOn: Date | string
  isDeleted: boolean
}

export type ProductData = {
  success: boolean
  message: string
  data: [Product]
}
