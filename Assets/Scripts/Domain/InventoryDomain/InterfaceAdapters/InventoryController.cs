using UCARPG.Domain.InventoryDomain.UseCases;

namespace UCARPG.Domain.InventoryDomain.InterfaceAdapters
{
    public class InventoryController
    {

        private StoreItemUseCase _storeItemUseCase;
        private EquipNewItemUseCase _equipNewItemUseCase;
        private EquipItemFromStorageUseCase _equipItemFromStorageUseCase;
        private UnequipItemUseCase _unequipItemUseCase;
        private UseConsumableUseCase _useConsumableUseCase;
        private DiscardItemUseCase _discardItemUseCase;
        private IsExistItemUseCase _isExistItemUseCase;
        private GetItemConfigIdUseCase _getItemConfigIdUseCase;
        private CanStoreItemUseCase _canStoreItemUseCase;
        private CanEquipItemFromStorageUseCase _canEquipItemFromStorageUseCase;

        public InventoryController(StoreItemUseCase storeItemUseCase,
                                   EquipNewItemUseCase equipNewItemUseCase,
                                   EquipItemFromStorageUseCase equipItemFromStorageUseCase,
                                   UnequipItemUseCase unequipItemUseCase,
                                   UseConsumableUseCase useConsumableUseCase,
                                   DiscardItemUseCase discardItemUseCase,
                                   IsExistItemUseCase isExistItemUseCase,
                                   GetItemConfigIdUseCase getItemConfigIdUseCase,
                                   CanStoreItemUseCase canStoreItemUseCase,
                                   CanEquipItemFromStorageUseCase canEquipItemFromStorageUseCase)
        {
            _storeItemUseCase = storeItemUseCase;
            _equipNewItemUseCase = equipNewItemUseCase;
            _equipItemFromStorageUseCase = equipItemFromStorageUseCase;
            _unequipItemUseCase = unequipItemUseCase;
            _useConsumableUseCase = useConsumableUseCase;
            _discardItemUseCase = discardItemUseCase;
            _isExistItemUseCase = isExistItemUseCase;
            _getItemConfigIdUseCase = getItemConfigIdUseCase;
            _canStoreItemUseCase = canStoreItemUseCase;
            _canEquipItemFromStorageUseCase = canEquipItemFromStorageUseCase;
        }

        public void StoreItem(string configId, string type, int capacity, int count)
        {
            _storeItemUseCase.Execute(configId, type, capacity, count);
        }

        public void EquipNewItem(string configId, string type, int capacity, int count)
        {
            _equipNewItemUseCase.Execute(configId, type, capacity, count);
        }

        public void EquipItemFromStorage(int index)
        {
            _equipItemFromStorageUseCase.Execute(index);
        }

        public void UnequipItem(string type, int index)
        {
            _unequipItemUseCase.Execute(type, index);
        }

        public void UseConsumable(int index)
        {
            _useConsumableUseCase.Execute(index);
        }

        public (string ConfigId, int Count) DiscardItem(int index)
        {
            return _discardItemUseCase.Execute(index);
        }

        public bool IsExistItem(string setName, int index)
        {
            return _isExistItemUseCase.Execute(setName, index);
        }

        public string GetItemConfigId(string setName, int index)
        {
            return _getItemConfigIdUseCase.Execute(setName, index);
        }

        public bool CanStoreItem(string configId, int count)
        {
            return _canStoreItemUseCase.Execute(configId, count);
        }

        public bool CanEquipItemFromStorage(int index)
        {
            return _canEquipItemFromStorageUseCase.Execute(index);
        }
    }
}