export type Sales = {
  id: string
  saledOn: Date | string
  customerId: string
  total: number
}

export type SaleDetail = {
  productId: string
  saleId: string
  quantity: number
  saledPrice: number
}

export type Sale = {
  id: string
  saledOn: Date | string
  customerId: string
  total: number
  saleDetails: SaleDetail[]
}

export type SaleData = {
  success: boolean
  message: string
  data: Sales[]
}
