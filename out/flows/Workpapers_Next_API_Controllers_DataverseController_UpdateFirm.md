[web] PUT /api/dataverse/firms/{dataverseId}  (Workpapers.Next.API.Controllers.DataverseController.UpdateFirm)  [L275–L319] status=200 [auth=AuthorizationPolicies.M2M]
  └─ maps_to FirmLightDto [L316]
  └─ maps_to FirmLightWithSubscriptionsDto [L315]
  └─ maps_to FirmRestrictedUpdateModel (var firmModel) [L284]
    └─ automapper.registration DataverseMappingProfile (DataverseFirmUpdateModel->FirmRestrictedUpdateModel) [L114]
  └─ uses_service IMapper
    └─ method Map [L284]
      └─ ... (no implementation details available)
  └─ uses_service UnitOfWork
    └─ method Table [L279]
      └─ ... (no implementation details available)

