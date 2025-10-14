[web] PUT /api/dataverse/firms/{dataverseId}  (Workpapers.Next.API.Controllers.DataverseController.UpdateFirm)  [L274–L318] [auth=AuthorizationPolicies.M2M]
  └─ maps_to FirmLightDto [L315]
  └─ maps_to FirmLightWithSubscriptionsDto [L314]
  └─ maps_to FirmRestrictedUpdateModel (var firmModel) [L283]
    └─ automapper.registration DataverseMappingProfile (DataverseFirmUpdateModel->FirmRestrictedUpdateModel) [L114]
  └─ uses_service IMapper
    └─ method Map [L283]
  └─ uses_service UnitOfWork
    └─ method Table [L278]

