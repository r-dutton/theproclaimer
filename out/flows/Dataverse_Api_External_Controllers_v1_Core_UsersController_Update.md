[web] PUT /core/v1/users/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.UsersController.Update)  [L160–L169] status=200 [auth=Authentication.CoreWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method PutSerialisedModel [L167]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.PutSerialisedModel [L12-L74]

