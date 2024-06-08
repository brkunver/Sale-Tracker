import { addNewProduct } from "@/utils/ApiCalls/productApiCalls"
import { Button } from "../ui/button"
import { Input } from "../ui/input"
import { useMutation, useQueryClient } from "@tanstack/react-query"
import { useNavigate } from "react-router-dom"

export default function AddProduct() {
  const navigate = useNavigate()
  const queryClient = useQueryClient()
  const mutation = useMutation({
    mutationFn: async (formData: FormData) => {
      return await addNewProduct(formData)
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["products"] })
      clearInputs()
      navigate("/products")
    },
  })

  function formSubmitHandler(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault()
    let formData = new FormData(event.currentTarget)
    formData.set("Price", (formData.get("Price") as string).replace(".", ","))
    mutation.mutate(formData)
  }

  function clearInputs() {
    const inputs = document.querySelectorAll('input[type="text"], input[type="file"]')
    inputs.forEach((input) => ((input as HTMLInputElement).value = ""))
  }

  return (
    <form onSubmit={formSubmitHandler} className="mx-auto flex flex-col px-8 gap-2 border rounded py-10">
      <h2 className="text-center font-semibold text-lg mb-2">Add New Product</h2>
      <div className="grid lg:grid-cols-2">
        <span className="font-semibold">Select a jpg image or leave empty</span>
        <Input type="file" name="FormFile" accept="image/jpeg" />
      </div>
      <Input type="text" required aria-required name="Name" placeholder="Product Name" />
      <Input type="text" required aria-required name="Description" placeholder="Description" />
      <Input
        type="text"
        required
        aria-required
        pattern="^\d{1,10}([.,]\d{1,2})?$"
        name="Price"
        placeholder="Price, ex : 24,50"
      />
      <Button className="w-full mx-auto px-2" type="submit">
        Add Product
      </Button>
    </form>
  )
}
