[web] GET /api/accounting/assets/assets/{id}/split-info  (Cirrus.Api.Controllers.Accounting.Assets.AssetSplitController.GetSplitInfo)  [L39–L45] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to AssetSplitInfoDto [L42]
    └─ automapper.registration CirrusMappingProfile (Asset->AssetSplitInfoDto) [L896]
  └─ calls AssetRepository.ReadQuery [L42]
  └─ query Asset [L42]
    └─ reads_from Assets
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Asset writes=0 reads=1
    └─ mappings 1
      └─ AssetSplitInfoDto

