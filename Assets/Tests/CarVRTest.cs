using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarVr;
using UnityEngine.UI;
using NUnit.Framework;
using TMPro;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class CarVRTest : MonoBehaviour
{
    private CatalogManager catalogManager;
    private CheckInternet checkInternet;
    private UIManager uiManager;

    [SetUp]
    public void Setup()
    {
        // Initialisation du CatalogManager
        GameObject catalogManagerObject = new GameObject();
        catalogManager = catalogManagerObject.AddComponent<CatalogManager>();
        CatalogManager.instance = catalogManager;

        // Initialisation du CheckInternet
        GameObject checkInternetObject = new GameObject();
        checkInternet = checkInternetObject.AddComponent<CheckInternet>();


    }

    [TearDown]
    public void TearDown()
    {
        // Nettoyage apr�s chaque test
        if (catalogManager != null)
            Object.Destroy(catalogManager.gameObject);

        if (checkInternet != null)
            Object.Destroy(checkInternet.gameObject);
    }

    [UnityTest]
    public IEnumerator TestAPIRequest()
    {
        // Appeler la m�thode GetCarData
        yield return catalogManager.StartCoroutine(catalogManager.GetCarData());

        // V�rifier que la liste a �t� remplie
        Assert.IsFalse(catalogManager.carList.Count == 0, "La liste des voitures doit �tre remplie apr�s l'appel � l'API.");
    }

    [UnityTest]
    public IEnumerator TestVehicleSwitching()
    {
        SceneManager.LoadScene("New Scene", LoadSceneMode.Single);
        yield return new WaitForSeconds(2); // Attendre une frame pour assurer le chargement complet

        uiManager = GameObject.FindAnyObjectByType<UIManager>();

        Assert.AreEqual("AMC", uiManager.currentCar.name, "Le premier v�hicule devrait �tre Car1");

        // Passer au v�hicule suivant
        uiManager.NextVehicle();

        // V�rifier que le v�hicule actuel a chang�
        yield return null; // Attendre un frame pour que la m�thode NextVehicle soit termin�e
        Assert.AreEqual("Toyota", uiManager.currentCar.name, "Le v�hicule devrait maintenant �tre Car2");

        // Revenir au v�hicule pr�c�dent
        uiManager.PreviousVehicle();

        // V�rifier que le v�hicule actuel est revenu � l'original
        yield return null;
        Assert.AreEqual("AMC", uiManager.currentCar.name, "Le v�hicule devrait �tre revenu � Car1");
    }


    [UnityTest]
    public IEnumerator TestInternetConnection()
    {
        // V�rifier l'�tat initial de la connexion
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Assert.Fail("Pas de connexion Internet.");
        }

        // V�rifier la connexion Internet via l'appel � VerifyInternetConnection
        yield return checkInternet.StartCoroutine(checkInternet.VerifyInternetConnection());

        // Si aucune erreur, le test passe
        Assert.Pass("Connexion Internet r�ussie.");
    }
}
