﻿using Microsoft.Azure.Documents.Spatial;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using trifenix.connect.agro.external.main;
using trifenix.connect.agro.interfaces;
using trifenix.connect.agro.interfaces.external;
using trifenix.connect.agro_model;
using trifenix.connect.agro_model_input;
using trifenix.connect.interfaces;
using trifenix.connect.interfaces.db.cosmos;
using trifenix.connect.interfaces.external;
using trifenix.connect.interfaces.upload;
using trifenix.connect.mdm.containers;
using trifenix.connect.mdm.enums;

namespace trifenix.connect.agro.external
{

    /// <summary>
    /// Todos las funciones necesarias para interactuar con eventos registrados en el monitoreo.
    /// </summary>
    public class NotificationEventOperations<T> : MainOperation<NotificationEvent, NotificationEventInput,T>, IGenericOperation<NotificationEvent, NotificationEventInput> {
        
        private readonly ICommonQueries commonQueries;
        private readonly IEmail email;
        private readonly IUploadImage uploadImage;
        private readonly IWeatherApi weather;

        public NotificationEventOperations(IMainGenericDb<NotificationEvent> repo, IAgroSearch<T> search, ICommonQueries commonQueries, IEmail email, IUploadImage uploadImage, IWeatherApi weather, ICommonDbOperations<NotificationEvent> commonDb, IValidatorAttributes<NotificationEventInput, NotificationEvent> validator) : base(repo, search, commonDb, validator) {
            this.commonQueries = commonQueries;
            this.email = email;
            this.uploadImage = uploadImage;
            this.weather = weather;
        }

        //TODO: remove
        public Task Remove(string id) {
            throw new NotImplementedException();
        }

     

        public async Task<ExtPostContainer<string>> Save(NotificationEvent notificationEvent) {
            //TODO: Revisar
            var picturePath = await uploadImage.UploadImageBase64(notificationEvent.PicturePath);
            notificationEvent.PicturePath = picturePath;
            await repo.CreateUpdate(notificationEvent);
            search.AddDocument(notificationEvent);

            //TODO: Definir el origen de la lista de idsRoles
            var usersEmails = await commonQueries.GetUsersMailsFromRoles(new List<string> { "24beac75d4bb4f8d8fae8373426af780" });
            email.SendEmail(usersEmails, "Notificacion",
                $@"<html>
                    <body>
                        <p> Estimado(a), </p>
                        <p> Llego una notificacion </p>
                        <img src='{picturePath}' style='width:50%;height:auto;'>
                        <p> Atentamente,<br> -Aresa </br></p>
                    </body>
                </html>");
            return new ExtPostContainer<string> {
                IdRelated = notificationEvent.Id,
                MessageResult = ExtMessageResult.Ok
            };
        }

        public async Task<ExtPostContainer<string>> SaveInput(NotificationEventInput input, bool isBatch) {
            await Validate(input);
            var id = !string.IsNullOrWhiteSpace(input.Id) ? input.Id : Guid.NewGuid().ToString("N");
            NotificationEvent notification = new NotificationEvent {
                Id = id,
                Created = DateTime.Now,
                IdBarrack = input.IdBarrack,
                IdPhenologicalEvent = input.IdPhenologicalEvent,
                NotificationType = input.NotificationType,
                PicturePath = input.Base64,
                Description = input.Description,
            };
            if (input.Location != null) {
                notification.Location = new Point(input.Location.Longitude, input.Location.Latitude);
                notification.Weather = await weather.GetWeather((float)input.Location.Latitude, (float)input.Location.Longitude);
            }
            if (!isBatch)
                return await Save(notification);
            await repo.CreateEntityContainer(notification);
            return new ExtPostContainer<string> {
                IdRelated = id,
                MessageResult = ExtMessageResult.Ok
            };
        }

    }

}