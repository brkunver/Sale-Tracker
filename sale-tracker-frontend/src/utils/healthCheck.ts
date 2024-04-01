export async function healthCheck() {
  try {
    const response = await fetch(import.meta.env.VITE_API_URL + "/health")
    if (!response.ok) {
      return false
    }
    return true
  } catch (error) {
    return false
  }
}
