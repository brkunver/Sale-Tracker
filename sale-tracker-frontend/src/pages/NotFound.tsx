import { Link } from "react-router-dom"

function NotFound() {
  return (
    <main className="flex flex-col items-center pt-24 gap-6">
      <h1 className="text-center font-bold text-4xl">Page Not Found</h1>
      <Link to="/" className="text-center font-semibold text-lg underline hover:text-blue-500">
        Go to Home
      </Link>
    </main>
  )
}

export default NotFound
