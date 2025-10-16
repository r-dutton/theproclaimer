[web] GET /api/accounting/assets/asset-groups/{id}  (Cirrus.Api.Controllers.Accounting.Assets.AssetGroupController.Get)  [L41–L47] status=200 [auth=user]
  └─ maps_to AssetGroupDto [L44]
    └─ automapper.registration CirrusMappingProfile (AssetGroup->AssetGroupDto) [L874]
  └─ calls AssetGroupRepository.ReadQuery [L44]
  └─ query AssetGroup [L44]
    └─ reads_from AssetGroups
  └─ uses_service IControlledRepository<AssetGroup>
    └─ method ReadQuery [L44]
      └─ ... (no implementation details available)

