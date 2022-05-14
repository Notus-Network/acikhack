using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.SessionStorage;

namespace Notus.Functions
{
    public class General
    {
        private readonly ISessionStorageService sessionStorage;
        public General(ISessionStorageService sessionService)
        {
            sessionStorage = sessionService;
        }

        public async Task<bool> AddWalletToSession(string privateKey)
        {
            if (privateKey.Length != 64)
                return false;
            if (string.IsNullOrEmpty(privateKey))
                return false;

            Notus.Core.Variable.EccKeyPair pair = new Notus.Core.Variable.EccKeyPair() { PrivateKey = privateKey, CurveName = Notus.Core.Variable.Default_EccCurveName, PublicKey = Notus.Core.Wallet.ID.Generate(privateKey), WalletKey = Notus.Core.Wallet.ID.GetAddress(privateKey) };

            Notus.Web3.Application.LocalWalletList wallet = new Notus.Web3.Application.LocalWalletList()
            {
                WalletName = "My Wallet",
                WalletImage = "",
                Wallet = pair,
                DaysAgo = DateTime.Now
            };

            await sessionStorage.SetItemAsync("wallet", wallet);
            return true;
        }

        public async Task<Notus.Web3.Application.LocalWalletList> GetWalletFromSession()
        {
            if (await sessionStorage.ContainKeyAsync("wallet"))
                return await sessionStorage.GetItemAsync<Notus.Web3.Application.LocalWalletList>("wallet");

            return null;
        }

        public async Task Logout()
        {
            await sessionStorage.ClearAsync();
        }
    }
}
