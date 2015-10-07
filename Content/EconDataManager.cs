using System.Linq;

namespace Economy.scripts.EconConfig
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Sandbox.ModAPI;

    public static class EconDataManager
    {
        #region Load and save CONFIG

        public static string GetConfigFilename()
        {
            return string.Format("EconomyConfig_{0}.xml", Path.GetFileNameWithoutExtension(MyAPIGateway.Session.CurrentPath));
        }

        public static EconConfigStruct LoadConfig()
        {
            string filename = GetConfigFilename();
            EconConfigStruct config = null;

            if (!MyAPIGateway.Utilities.FileExistsInLocalStorage(filename, typeof(EconConfigStruct)))
            {
                config = InitConfig();
                ValidateAndUpdateConfig(config);
                return config;
            }

            TextReader reader = MyAPIGateway.Utilities.ReadFileInLocalStorage(filename, typeof(EconConfigStruct));

            var xmlText = reader.ReadToEnd();
            reader.Close();

            if (string.IsNullOrWhiteSpace(xmlText))
            {
                config = InitConfig();
                ValidateAndUpdateConfig(config);
                return config;
            }

            try
            {
                config = MyAPIGateway.Utilities.SerializeFromXML<EconConfigStruct>(xmlText);
                EconomyScript.Instance.ServerLogger.Write("Загрузка существующего EconConfigStruct.");
            }
            catch
            {
                // config failed to deserialize.
                EconomyScript.Instance.ServerLogger.Write("Не удалось десериализовать EconConfigStruct. Создание нового EconConfigStruct.");
                config = InitConfig();
            }

            ValidateAndUpdateConfig(config);
            return config;
        }

        private static void ValidateAndUpdateConfig(EconConfigStruct config)
        {
            EconomyScript.Instance.ServerLogger.Write("Проверки и обновления Config.");

            // Sync in whatever is defined in the game (may contain new cubes, and modded cubes).
            MarketManager.SyncMarketItems(ref config.DefaultPrices);
        }

        private static EconConfigStruct InitConfig()
        {
            EconomyScript.Instance.ServerLogger.Write("Создание нового EconConfigStruct.");
            EconConfigStruct config = new EconConfigStruct();
            config.DefaultPrices = new List<MarketStruct>();

            #region Default prices in raw Xml.

            const string xmlText = @"<MarketConfig>
  <MarketItems>
    <MarketStruct>
      <TypeId>MyObjectBuilder_AmmoMagazine</TypeId>
      <SubtypeName>NATO_5p56x45mm</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>1.501335</SellPrice>
      <BuyPrice>1.501335</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_AmmoMagazine</TypeId>
      <SubtypeName>NATO_25x184mm</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>26.294424</SellPrice>
      <BuyPrice>26.294424</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_AmmoMagazine</TypeId>
      <SubtypeName>Missile200mm</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>10.094312</SellPrice>
      <BuyPrice>10.094312</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>Construction</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.001744</SellPrice>
      <BuyPrice>0.001744</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>MetalGrid</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.146479</SellPrice>
      <BuyPrice>0.146479</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>InteriorPlate</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.000610</SellPrice>
      <BuyPrice>0.000610</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>SteelPlate</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.003662</SellPrice>
      <BuyPrice>0.003662</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>Girder</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>1.40</SellPrice>
      <BuyPrice>1.24</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>SmallTube</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.000872</SellPrice>
      <BuyPrice>0.000872</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>LargeTube</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.005232</SellPrice>
      <BuyPrice>0.005232</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>Motor</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.058600</SellPrice>
      <BuyPrice>0.058600</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>Display</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.011490</SellPrice>
      <BuyPrice>0.011490</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>BulletproofGlass</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.033946</SellPrice>
      <BuyPrice>0.033946</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>Computer</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.000540</SellPrice>
      <BuyPrice>0.000540</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>Reactor</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.041211</SellPrice>
      <BuyPrice>0.041211</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>Thrust</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.372646</SellPrice>
      <BuyPrice>0.372646</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>GravityGenerator</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>5.101434</SellPrice>
      <BuyPrice>5.101434</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>Medical</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.917108</SellPrice>
      <BuyPrice>0.917108</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>RadioCommunication</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.003658</SellPrice>
      <BuyPrice>0.003658</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>Detector</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.166209</SellPrice>
      <BuyPrice>0.166209</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>Explosives</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>14.991038</SellPrice>
      <BuyPrice>14.991038</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>SolarCell</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.128329</SellPrice>
      <BuyPrice>0.128329</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Component</TypeId>
      <SubtypeName>PowerCell</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.026052</SellPrice>
      <BuyPrice>0.026052</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ore</TypeId>
      <SubtypeName>Stone</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.0000002</SellPrice>
      <BuyPrice>0.0000002</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ore</TypeId>
      <SubtypeName>Iron</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.0001161</SellPrice>
      <BuyPrice>0.0001161</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ore</TypeId>
      <SubtypeName>Nickel</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.0041987</SellPrice>
      <BuyPrice>0.0041987</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ore</TypeId>
      <SubtypeName>Cobalt</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.0062032</SellPrice>
      <BuyPrice>0.0062032</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ore</TypeId>
      <SubtypeName>Magnesium</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.0500827</SellPrice>
      <BuyPrice>0.0500827</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ore</TypeId>
      <SubtypeName>Silicon</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.0015069</SellPrice>
      <BuyPrice>0.0015069</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ore</TypeId>
      <SubtypeName>Silver</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.0006435</SellPrice>
      <BuyPrice>0.0006435</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ore</TypeId>
      <SubtypeName>Gold</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.0001776</SellPrice>
      <BuyPrice>0.0001776</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ore</TypeId>
      <SubtypeName>Platinum</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.0015710</SellPrice>
      <BuyPrice>0.0015710</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ore</TypeId>
      <SubtypeName>Uranium</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.0665000</SellPrice>
      <BuyPrice>0.0665000</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ingot</TypeId>
      <SubtypeName>Stone</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.000202550</SellPrice>
      <BuyPrice>0.000202550</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ingot</TypeId>
      <SubtypeName>Iron</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.000174563</SellPrice>
      <BuyPrice>0.000174563</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ingot</TypeId>
      <SubtypeName>Nickel</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.011049303</SellPrice>
      <BuyPrice>0.011049303</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ingot</TypeId>
      <SubtypeName>Cobalt</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.021765651</SellPrice>
      <BuyPrice>0.021765651</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ingot</TypeId>
      <SubtypeName>Magnesium</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>7.531237340</SellPrice>
      <BuyPrice>7.531237340</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ingot</TypeId>
      <SubtypeName>Silicon</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.002265985</SellPrice>
      <BuyPrice>0.002265985</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ingot</TypeId>
      <SubtypeName>Silver</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.006773230</SellPrice>
      <BuyPrice>0.006773230</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ingot</TypeId>
      <SubtypeName>Gold</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.018691102</SellPrice>
      <BuyPrice>0.018691102</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ingot</TypeId>
      <SubtypeName>Platinum</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.330747318</SellPrice>
      <BuyPrice>0.330747318</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ingot</TypeId>
      <SubtypeName>Uranium</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>10</SellPrice>
      <BuyPrice>10</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_PhysicalGunObject</TypeId>
      <SubtypeName>AutomaticRifleItem</SubtypeName>
      <Quantity>100</Quantity>
      <SellPrice>0.011573</SellPrice>
      <BuyPrice>0.011573</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_OxygenContainerObject</TypeId>
      <SubtypeName>OxygenBottle</SubtypeName>
      <Quantity>100</Quantity>
      <SellPrice>0.413176</SellPrice>
      <BuyPrice>0.413176</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_PhysicalGunObject</TypeId>
      <SubtypeName>WelderItem</SubtypeName>
      <Quantity>100</Quantity>
      <SellPrice>1</SellPrice>
      <BuyPrice>1</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_PhysicalGunObject</TypeId>
      <SubtypeName>AngleGrinderItem</SubtypeName>
      <Quantity>100</Quantity>
      <SellPrice>1</SellPrice>
      <BuyPrice>1</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_PhysicalGunObject</TypeId>
      <SubtypeName>HandDrillItem</SubtypeName>
      <Quantity>100</Quantity>
      <SellPrice>1</SellPrice>
      <BuyPrice>1</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ore</TypeId>
      <SubtypeName>Scrap</SubtypeName>
      <Quantity>100</Quantity>
      <SellPrice>0.000116105</SellPrice>
      <BuyPrice>0.000116105</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ingot</TypeId>
      <SubtypeName>Scrap</SubtypeName>
      <Quantity>0</Quantity>
      <SellPrice>0.000116105</SellPrice>
      <BuyPrice>0.000116105</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ore</TypeId>
      <SubtypeName>Ice</SubtypeName>
      <Quantity>1000</Quantity>
      <SellPrice>0.0002565</SellPrice>
      <BuyPrice>0.0002565</BuyPrice>
      <IsBlacklisted>false</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_Ore</TypeId>
      <SubtypeName>Organic</SubtypeName>
      <Quantity>100</Quantity>
      <SellPrice>1</SellPrice>
      <BuyPrice>1</BuyPrice>
      <IsBlacklisted>true</IsBlacklisted>
    </MarketStruct>
    <MarketStruct>
      <TypeId>MyObjectBuilder_GasContainerObject</TypeId>
      <SubtypeName>HydrogenBottle</SubtypeName>
      <Quantity>100</Quantity>
      <SellPrice>10</SellPrice>
      <BuyPrice>10</BuyPrice>
      <IsBlacklisted>true</IsBlacklisted>
    </MarketStruct>
  </MarketItems>
</MarketConfig>";

            // anything not in this Xml, will be added in via ValidateAndUpdateConfig() and SyncMarketItems().

            #endregion

            try
            {
                var items = MyAPIGateway.Utilities.SerializeFromXML<MarketConfig>(xmlText);
                config.DefaultPrices = items.MarketItems;

            }
            catch (Exception ex)
            {
                // This catches our stupidity and two left handed typing skills.
                // Check the Server logs to make sure this data loaded.
                EconomyScript.Instance.ServerLogger.WriteException(ex);
            }

            return config;
        }

        public static void SaveConfig(EconConfigStruct config)
        {
            string filename = GetConfigFilename();
            TextWriter writer = MyAPIGateway.Utilities.WriteFileInLocalStorage(filename, typeof(EconConfigStruct));
            writer.Write(MyAPIGateway.Utilities.SerializeToXML<EconConfigStruct>(config));
            writer.Flush();
            writer.Close();
        }

        #endregion

        #region Load and save DATA

        public static string GetDataFilename()
        {
            return string.Format("EconomyData_{0}.xml", Path.GetFileNameWithoutExtension(MyAPIGateway.Session.CurrentPath));
        }

        public static EconDataStruct LoadData(List<MarketStruct> defaultPrices)
        {
            string filename = GetDataFilename();
            EconDataStruct data = null;

            if (!MyAPIGateway.Utilities.FileExistsInLocalStorage(filename, typeof(EconDataStruct)))
            {
                data = InitData();
                if (LoadOldData(data))
                    SaveData(data); // need to save current data as old files have just been deleted.

                ValidateAndUpdateData(data, defaultPrices);
                return data;
            }

            TextReader reader = MyAPIGateway.Utilities.ReadFileInLocalStorage(filename, typeof(EconDataStruct));

            var xmlText = reader.ReadToEnd();
            reader.Close();

            if (string.IsNullOrWhiteSpace(xmlText))
            {
                data = InitData();
                if (LoadOldData(data))
                    SaveData(data); // need to save current data as old files have just been deleted.

                ValidateAndUpdateData(data, defaultPrices);
                return data;
            }

            try
            {
                data = MyAPIGateway.Utilities.SerializeFromXML<EconDataStruct>(xmlText);
                EconomyScript.Instance.ServerLogger.Write("Загрузка существующего EconDataStruct.");
            }
            catch
            {
                // data failed to deserialize.
                EconomyScript.Instance.ServerLogger.Write("Не удалось десериализовать EconDataStruct. Создание нового EconDataStruct.");
                data = InitData();
            }

            ValidateAndUpdateData(data, defaultPrices);

            return data;
        }

        private static void ValidateAndUpdateData(EconDataStruct data, List<MarketStruct> defaultPrices)
        {
            EconomyScript.Instance.ServerLogger.Write("Проверки и обновления данных.");

            // Add missing items that are covered by Default items.
            foreach (var defaultItem in defaultPrices)
            {
                if (!data.MarketItems.Any(e => e.TypeId.Equals(defaultItem.TypeId) && e.SubtypeName.Equals(defaultItem.SubtypeName)))
                {
                    data.MarketItems.Add(new MarketStruct { TypeId = defaultItem.TypeId, SubtypeName = defaultItem.SubtypeName, BuyPrice = defaultItem.BuyPrice, SellPrice = defaultItem.SellPrice, IsBlacklisted = defaultItem.IsBlacklisted, Quantity = defaultItem.Quantity });
                    EconomyScript.Instance.ServerLogger.Write("MarketItem Добавление пункт По умолчанию: {0} {1}.", defaultItem.TypeId, defaultItem.SubtypeName);
                }
            }

            // Don't have to call this anymore, as the Config will have called it for the Default item prices.
            //MarketManager.SyncMarketItems(ref data.MarketItems);

            // Buy/Sell - check we have our NPC banker ready
            NpcMerchantManager.VerifyAndCreate(data);
        }

        private static bool LoadOldData(EconDataStruct data)
        {
            string filename;
            bool oldLoaded = false;

            filename = MarketManager.GetContentFilename();
            if (MyAPIGateway.Utilities.FileExistsInLocalStorage(filename, typeof(MarketManager)))
            {
                EconomyScript.Instance.ServerLogger.Write("Загрузка старую версию файла MarketManagement: {0}", filename);
                var marketConfigData = MarketManager.LoadContent();
                data.MarketItems = marketConfigData.MarketItems;
                oldLoaded = true;
                MyAPIGateway.Utilities.DeleteFileInLocalStorage(filename, typeof(MarketManager));
            }

            filename = BankManagement.GetContentFilename();
            if (MyAPIGateway.Utilities.FileExistsInLocalStorage(filename, typeof(BankConfig)))
            {
                EconomyScript.Instance.ServerLogger.Write("Загрузка старую версию файла BankConfig: {0}", filename);
                var bankConfigData = BankManagement.LoadContent();
                data.Accounts = bankConfigData.Accounts;
                oldLoaded = true;
                MyAPIGateway.Utilities.DeleteFileInLocalStorage(filename, typeof(BankConfig));
            }

            return oldLoaded;
        }

        private static EconDataStruct InitData()
        {
            EconomyScript.Instance.ServerLogger.Write("Создание нового EconDataStruct.");
            EconDataStruct data = new EconDataStruct();
            data.Accounts = new List<BankAccountStruct>();
            data.MarketItems = new List<MarketStruct>();
            return data;
        }

        public static void SaveData(EconDataStruct data)
        {
            string filename = GetDataFilename();
            TextWriter writer = MyAPIGateway.Utilities.WriteFileInLocalStorage(filename, typeof(EconDataStruct));
            writer.Write(MyAPIGateway.Utilities.SerializeToXML<EconDataStruct>(data));
            writer.Flush();
            writer.Close();
        }

        #endregion
    }
}
