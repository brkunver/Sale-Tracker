import localforage from "localforage"

export default async function isDemo() {
  try {
    const demoStatus = (await localforage.getItem("demo")) as boolean
    return demoStatus
  } catch (error) {
    return false
  }
}
