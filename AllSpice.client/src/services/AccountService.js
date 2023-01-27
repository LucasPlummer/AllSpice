import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getAccountRecipes() {
    const res = await api.get(`api/recipes`);
    let recipes = res.data
    let filtered = recipes.filter(r => r.creatorId == AppState.account.id)
    AppState.recipes = filtered
  }
}

export const accountService = new AccountService()
