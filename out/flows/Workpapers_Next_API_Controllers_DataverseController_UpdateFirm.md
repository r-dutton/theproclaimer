[web] PUT /api/dataverse/firms/{dataverseId}  (Workpapers.Next.API.Controllers.DataverseController.UpdateFirm)  [L275–L319] status=200 [auth=AuthorizationPolicies.M2M]
  └─ maps_to FirmLightDto [L316]
  └─ maps_to FirmLightWithSubscriptionsDto [L315]
  └─ maps_to FirmRestrictedUpdateModel (var firmModel) [L284]
    └─ automapper.registration DataverseMappingProfile (DataverseFirmUpdateModel->FirmRestrictedUpdateModel) [L114]
  └─ uses_service UnitOfWork
    └─ method Table [L279]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 3
      └─ FirmLightDto
      └─ FirmLightWithSubscriptionsDto
      └─ FirmRestrictedUpdateModel

