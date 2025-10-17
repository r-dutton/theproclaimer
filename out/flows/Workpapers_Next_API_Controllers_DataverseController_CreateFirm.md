[web] POST /api/dataverse/firms  (Workpapers.Next.API.Controllers.DataverseController.CreateFirm)  [L234–L267] status=201 [auth=AuthorizationPolicies.M2M]
  └─ maps_to FirmLightWithSubscriptionsDto [L266]
  └─ maps_to FirmCreateModel (var firmModel) [L254]
    └─ automapper.registration DataverseMappingProfile (DataverseFirmCreateModel->FirmCreateModel) [L111]
  └─ uses_service UnitOfWork
    └─ method Table [L241]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 2
      └─ FirmCreateModel
      └─ FirmLightWithSubscriptionsDto

