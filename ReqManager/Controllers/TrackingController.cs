﻿using ReqManager.Entities.Artifact;
using ReqManager.Entities.Link;
using ReqManager.Entities.Project;
using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Directories.Interfaces;
using ReqManager.Services.Link.Interfaces;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    public class TrackingController : ControlAcessController<TrackingViewModel>
    {
        #region Propriets

        private IScanDirectoryService directory { get; set; }
        private IRequirementService requirement { get; set; }
        private IProjectArtifactService artifact { get; set; }
        private ILinkBetweenRequirementsService linkReq { get; set; }
        private ILinkBetweenRequirementsArtifactsService linkArt { get; set; }
        private IProjectRequirementsService reqProj { get; set; }
        private IProjectService project { get; set; }
        private string path { get; set; }

        #endregion

        #region Construtor

        public TrackingController(
            IRequirementService requirement,
            IProjectArtifactService artifact,
            ILinkBetweenRequirementsService linkReq,
            ILinkBetweenRequirementsArtifactsService linkArt,
            IScanDirectoryService directory,
            IProjectService project,
            IProjectRequirementsService reqProj)
        {
            this.reqProj = reqProj;
            this.directory = directory;
            this.requirement = requirement;
            this.artifact = artifact;
            this.linkReq = linkReq;
            this.linkArt = linkArt;
            this.project = project;
        }

        #endregion

        #region Requirements

        public ActionResult TrackingProjectRequirement()
        {
            try
            {
                ViewData.Add("Requirements", Enumerable.Empty<SelectListItem>());
                ViewData.Add("Project", new SelectList(project.getAll(), "ProjectID", "DisplayName"));

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult TrackingRequirement(int? id)
        {
            try
            {
                RequirementEntity req = requirement.get(Convert.ToInt32(id));

                List<RequirementEntity> reqList = new List<RequirementEntity>()
                    {
                        req
                    };

                ViewBag.Title = "Track Project Requirement " + req.code;

                ViewData.Add("Requirements", new SelectList(reqList.AsEnumerable(), "RequirementID", "DisplayName", id));
                ViewData.Add("Project", new SelectList(reqProj.getAll().Where(pr => pr.RequirementID.Equals(req.RequirementID)).Select(p => p.Project), "ProjectID", "DisplayName"));

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult TrackingRequirements(string Requirements, string Path, string Project)
        {
            try
            {
                if (reqProj.isTraceable(Convert.ToInt32(Project), Convert.ToInt32(Requirements)))
                {
                    RequirementEntity req = requirement.get(Convert.ToInt32(Requirements));
                    reqProj.isTraceable(Convert.ToInt32(Project), Convert.ToInt32(Requirements));



                    string[] requirements = { req.code };
                    List<string> files = directory.findFile(requirements, Path);

                    JsonResult json = Json(files, JsonRequestBehavior.AllowGet);
                    return json;
                }
                else
                {
                    throw new Exception("This Requirement is not traceable for that Project!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Artifacts

        public ActionResult TrackingProjectArtifact()
        {
            try
            {
                ViewData.Add("ProjectID", new SelectList(project.getAll(), "ProjectID", "DisplayName"));

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult TrackingArtifact(int? id)
        {
            try
            {
                ProjectArtifactEntity art = artifact.get(id);

                List<ProjectArtifactEntity> artList = new List<ProjectArtifactEntity>()
                    {
                        art
                    };

                List<ProjectEntity> prjList = new List<ProjectEntity>()
                    {
                        art.Project
                    };

                ViewBag.Title = "Track Project Requirement " + art.code;

                ViewData.Add("Artifacts", new SelectList(artList.AsEnumerable(), "ProjectArtifactID", "DisplayName", id));
                ViewData.Add("ProjectID", new SelectList(prjList.AsEnumerable(), "ProjectID", "DisplayName", art.ProjectID));
                ViewData.Add("Path", new SelectList(directory.getFolders(art.Project.pathForTraceability)));

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult TrackingArtifacts(string item, string Path)
        {
            try
            {
                ProjectArtifactEntity art = artifact.get(Convert.ToInt32(item));

                string[] artifacts = { art.code };
                List<string> files = directory.findFile(artifacts, Path);

                JsonResult json = Json(files, JsonRequestBehavior.AllowGet);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Link between Requirements

        public ActionResult TrackingLinkBetweenRequirement()
        {
            try
            {
                ViewData.Add("Project", new SelectList(project.getAll(), "ProjectID", "DisplayName"));
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult TrackingLinkBetweenRequirements(string Link, string Path)
        {
            try
            {
                string[] itens = { linkReq.get(Convert.ToInt32(Link)).code };
                List<string> files = directory.findFile(itens, Path);

                JsonResult json = Json(files, JsonRequestBehavior.AllowGet);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Link between Requirements and Artifacts

        public ActionResult TrackingLinkBetweenRequirementArtifact()
        {
            try
            {
                ViewData.Add("Project", new SelectList(project.getAll(), "ProjectID", "DisplayName"));
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult TrackingLinkBetweenRequirementArtifacts(string item, string Path)
        {
            try
            {
                string[] itens = { linkArt.get(Convert.ToInt32(item)).code };
                List<string> files = directory.findFile(itens, Path);

                JsonResult json = Json(files, JsonRequestBehavior.AllowGet);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Others

        [HttpPost]
        public void OpenFile(string file)
        {
            try
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = file;
                proc.Start();
                proc.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}