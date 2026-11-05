import 'package:flutter/material.dart';

import 'data/ConectionString.dart';

class DoctorsDetails extends StatelessWidget {

  final dynamic doctor;


  const DoctorsDetails(this.doctor, {super.key});

  @override
  Widget build(BuildContext context) {

    // Image Path
    final String imagePath =
        doctor['imageUrl']?.toString() ?? '';

    // Complete Image URL
    final String imageUrl = imagePath.isEmpty
        ? ''
        : Uri.parse(ConectionString.dashboardUrl)
        .resolve(imagePath)
        .toString();

    return Scaffold(

      appBar: AppBar(
        title: const Text('Product Details'),
        backgroundColor: Colors.indigo,
      ),

      body: SingleChildScrollView(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,

          children: [

            // Product Image
            SizedBox(
              width: double.infinity,
              height: 300,

              child: imageUrl.isNotEmpty
                  ? Image.network(
                imageUrl,
                fit: BoxFit.contain,

                errorBuilder: (context, error, stackTrace) {
                  return const Icon(
                    Icons.image_not_supported,
                    size: 100,
                  );
                },
              )
                  : const Icon(
                Icons.image,
                size: 100,
              ),
            ),

            const SizedBox(height: 20),

            Padding(
              padding: const EdgeInsets.all(16),

              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,

                children: [

                  // Product Name
                  Text(
                    doctor['name']?.toString() ?? '',
                    style: const TextStyle(
                      fontSize: 26,
                      fontWeight: FontWeight.bold,
                    ),
                  ),

                  const SizedBox(height: 15),

                  // Product Price
                  Text(
                    '${doctor['phone']} ',
                    style: const TextStyle(
                      fontSize: 24,
                      color: Colors.green,
                      fontWeight: FontWeight.bold,
                    ),
                  ),

                  const SizedBox(height: 20),

                  const Divider(),

                  const SizedBox(height: 15),

                  // Product Category
                  Text(
                    'Category Name: ${doctor['specialtyName'] ?? 'N/A'}',
                    style: const TextStyle(
                      fontSize: 18,
                    ),
                  ),

                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}