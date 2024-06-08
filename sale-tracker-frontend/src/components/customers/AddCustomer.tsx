import { addNewCustomer } from "../../utils/ApiCalls/customerApiCalls"
import { Button } from "../ui/button"
import { Input } from "../ui/input"
import { useMutation, useQueryClient } from "@tanstack/react-query"
import { useNavigate } from "react-router-dom"

function AddCustomer() {
  const navigate = useNavigate()
  const queryClient = useQueryClient()
  const mutation = useMutation({
    mutationFn: async (formData: FormData) => {
      return await addNewCustomer(formData)
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["customers"] })
      navigate("/customers")
    },
  })
  function formSubmitHandler(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault()
    let formData = new FormData(event.currentTarget)
    mutation.mutate(formData)
  }
  return (
    <form onSubmit={formSubmitHandler} className="mx-auto flex flex-col px-8 gap-2 border rounded py-10">
      <h2 className="text-center font-semibold text-lg mb-2">Add New Customer</h2>
      <Input type="text" required aria-required name="Name" placeholder="Customer Name" maxLength={100} />
      <Input type="text" required aria-required name="Phone" placeholder="Phone" pattern="\d{6,15}" />
      <Input type="text" required aria-required name="Address" placeholder="London" maxLength={255} />
      <Button className="w-full mx-auto px-2" type="submit">
        Add Customer
      </Button>
    </form>
  )
}

export default AddCustomer
