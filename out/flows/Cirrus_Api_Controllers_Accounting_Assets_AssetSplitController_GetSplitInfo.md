[web] GET /api/accounting/assets/assets/{id}/split-info  (Cirrus.Api.Controllers.Accounting.Assets.AssetSplitController.GetSplitInfo)  [L39–L45] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to AssetSplitInfoDto [L42]
    └─ automapper.registration CirrusMappingProfile (Asset->AssetSplitInfoDto) [L896]
  └─ calls AssetRepository.ReadQuery [L42]
  └─ query Asset [L42]
    └─ reads_from Assets
  └─ uses_service IControlledRepository<Asset>
    └─ method ReadQuery [L42]
      └─ ... (no implementation details available)

