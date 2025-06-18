using Xunit;
using Moq;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyHouse.Pages;
using MyHouse.Services;
using System.Collections.Generic;

public class ModeloTests {
    [Fact]
    public void ModeloCreateTeste() {
        Modelo modelo = Modelo.Create("d170", "Houses_test/Values/");
        Assert.NotNull(modelo.Personalization);
        modelo.Personalization["Sanitario"]["Ducha Higienica"] = true;
        modelo.Personalization["Acabamentos"]["Revestimento 2D"] = true;

        modelo.Store("Houses_test/Instances/");

        Modelo modelo2 = Modelo.Create("d170", "Houses_test/Instances/");

        Assert.IsType<Modelo>(modelo);
        Assert.Equal(modelo.Personalization, modelo2.Personalization);
    }
}